import {Component, OnInit, ViewChild, ElementRef, NgZone} from '@angular/core';
import {MapsAPILoader} from '@agm/core';
import {FormControl} from '@angular/forms';
import {} from '@types/googlemaps';

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
  directions = [];

  @ViewChild('originInput')
  public originElementRef: ElementRef;

  @ViewChild('destinationInput')
  public destinationElementRef: ElementRef;

  public renderOptions = {
    draggable: false,
    suppressMarkers: true,
  };

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

          console.log(originPlace.geometry.viewport.getCenter());
          const newOrigin = originPlace.geometry.viewport.getCenter();
          this.addOrigin(newOrigin);
          this.addMarkerOnMap(newOrigin);
          this.default.lat = newOrigin.lat();
          this.default.lng = newOrigin.lng();
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

          const newDestination = new google.maps.LatLng(destinationPlace.geometry.location.lat(), destinationPlace.geometry.location.lng());
          this.addDestination(newDestination);
          this.addMarkerOnMap(newDestination);
        });
      });
    });
  }

  addMarker(lat: number, lng: number) {
    const location = new google.maps.LatLng(lat, lng);
    if (this.origin == null) {
      this.addOrigin(location);
    } else if (this.destination == null) {
      this.addDestination(location);
    } else {
      this.addWaypoint(this.destination);
      this.addDestination(location);
    }
    this.addMarkerOnMap(location);
  }

  addMarkerOnMap(location: google.maps.LatLng) {
    this.markers.push({coords: location});
  }

  selectMarker(event) {
    this.selectedMarker = {
      lat: event.latitude,
      lng: event.longitude
    };
  }

  addOrigin(data: google.maps.LatLng) {
    this.origin = data;
  }

  addDestination(data: google.maps.LatLng) {
    this.destination = data;
  }

  addWaypoint(data: google.maps.LatLng) {
    this.waypoints.push({location: data});
  }

  onResponse(event) {
    console.log(event);
    this.directions = [];
    event.routes[0].legs.forEach(route => {
      this.directions.push({origin: route.start_address, destination: route.end_address});
    });
    console.log(this.directions);
  }
}
