import {Component, OnInit, ViewChild, ElementRef, NgZone} from '@angular/core';
import {MapsAPILoader, LatLng} from '@agm/core';
import {FormControl} from '@angular/forms';

declare var google;

@Component({
  selector: 'app-trail',
  templateUrl: './trail.component.html',
  styleUrls: ['./trail.component.scss']
})
export class TrailComponent implements OnInit {
  default = {lat: 49.257794, lng: 19.919636};
  zoom = 10;
  public searchControl: FormControl;
  origin;
  destination;
  waypoints: any = [];
  selectedMarker;
  markers = [];
  travelMode = 'WALKING';

  @ViewChild('originInput')
  public originElementRef: ElementRef;

  @ViewChild('destinationInput')
  public destinationElementRef: ElementRef;

  public renderOptions = {
    draggable: true,
  };

  public change(event: any) {
    this.waypoints = event.request.waypoints;
  }

  constructor(private mapsAPILoader: MapsAPILoader,
              private ngZone: NgZone) { }

  ngOnInit() {
    this.searchControl = new FormControl();
    this.mapsAPILoader.load().then(() => {
      const autocompleteOrigin = new google.maps.places.Autocomplete(this.originElementRef.nativeElement);
      autocompleteOrigin.addListener('place_changed', () => {
        this.ngZone.run(() => {
          const originPlace: google.maps.places.PlaceResult = autocompleteOrigin.getPlace();

          if (originPlace.geometry === undefined || originPlace.geometry === null) {
            return;
          }

          this.addOrigin(new google.maps.LatLng(originPlace.geometry.location.lat(), originPlace.geometry.location.lng()));
          this.addMarker(originPlace.geometry.location.lat(), originPlace.geometry.location.lng());
          this.default.lat = originPlace.geometry.location.lat();
          this.default.lng = originPlace.geometry.location.lng();
          this.zoom = 12;
        });
      });
      const autocompleteDestination = new google.maps.places.Autocomplete(this.destinationElementRef.nativeElement);
      autocompleteDestination.addListener('place_changed', () => {
        this.ngZone.run( () => {
          const destinationPlace: google.maps.places.PlaceResult = autocompleteDestination.getPlace();

          if (destinationPlace.geometry === undefined || destinationPlace.geometry === null) {
            return;
          }

          this.addDestination(new google.maps.LatLng(destinationPlace.geometry.location.lat(), destinationPlace.geometry.location.lng()));
        });
      });
    });
  }

  addMarker(lat: number, lng: number) {
    const latitude = lat;
    const longitude = lng;
    const newLatLng = new google.maps.LatLng(latitude, longitude);
    if (this.origin == null) {
      this.addOrigin(newLatLng);
      console.log(this.origin);
    } else if (this.destination == null) {
      this.addDestination(newLatLng);
      console.log(this.destination);
    } else {
      const oldLat = this.destination.lat;
      const oldLng = this.destination.lng;
      const newLocation = new google.maps.LatLng(oldLat, oldLng);
      this.addWaypoint(newLocation);
      this.addDestination(newLatLng);
      this.markers.push({ lat: latitude, lng: longitude, alpha: 1 });
      console.log(this.waypoints);
    }
  }

  selectMarker(event) {
    this.selectedMarker = {
      lat: event.latitude,
      lng: event.longitude
    };
  }

  addOrigin(data: LatLng) {
    this.origin = data;
  }

  addDestination(data: LatLng) {
    this.destination = data;
  }

  addWaypoint(data: LatLng) {
    this.waypoints.push({location: data});
  }

}
