import {Component, OnInit, ViewChild, ElementRef, NgZone} from '@angular/core';
import {MapsAPILoader} from '@agm/core';
import {FormControl} from '@angular/forms';
import {} from '@types/googlemaps';
import {Trail} from '../../dtos/trail';
import {TrailService} from '../../_services/trail.service';
import {ToastrService} from 'ngx-toastr';
import {Router} from '@angular/router';
import {MyLocation} from '../../dtos/myLocation';

declare const google: any;

@Component({
  selector: 'app-trail',
  templateUrl: './trail.component.html',
  styleUrls: ['./trail.component.scss']
})
export class TrailComponent implements OnInit {
  default = {lat: 49.257794, lng: 19.919636};
  zoom = 10;
  public searchControl: FormControl;
  elevator;
  origin;
  destination;
  originName;
  destinationName;
  chartData;
  waypoints: any = [];
  selectedMarker;
  markers = [];
  travelMode = 'WALKING';
  directions = [];

  @ViewChild('originInput')
  public originElementRef: ElementRef;

  @ViewChild('chart')
  chart;

  public renderOptions = {
    draggable: false,
    suppressMarkers: true,
  };

  constructor(private mapsAPILoader: MapsAPILoader,
              private ngZone: NgZone,
              private trailService: TrailService,
              private toastr: ToastrService,
              private router: Router) { }

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
          const newOrigin = originPlace.geometry.viewport.getCenter();
          this.addMarker(newOrigin.lat(), newOrigin.lng());
          this.default.lat = newOrigin.lat();
          this.default.lng = newOrigin.lng();
        });
      });
      this.elevator = new google.maps.ElevationService;
      google.charts.load('current', {packages: ['corechart']});
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
    this.markers.push({lat: location.lat(), lng: location.lng()});
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
    this.originName = this.directions[0]['origin'];
    this.destinationName = this.directions[this.directions.length - 1]['destination'];
    this.elevator.getElevationAlongPath({
      'path': this.markers,
      'samples': 256
    }, this.plotElevation);
  }

  saveTrail() {
    const trail = new Trail({longitude: this.origin.lng(), latitude: this.origin.lat()},
      new MyLocation(this.destination.lng(), this.destination.lat), this.waypoints);

    this.toastr.success('Created');
  }

  plotElevation(elevations, status) {
    const chartDiv = document.getElementById('elevation_chart');
    if (status !== 'OK') {
      chartDiv.innerHTML = 'Cannot show elevation: request failed because ' +
        status;
      return;
    }
    this.chart = new google.visualization.LineChart(chartDiv);

    this.chartData = new google.visualization.DataTable();
    this.chartData.addColumn('string', 'Sample');
    this.chartData.addColumn('number', 'Elevation');
    for (let i = 0; i < elevations.length; i++) {
      this.chartData.addRow(['', elevations[i].elevation]);
    }

    this.chart.draw(this.chartData, {
      height: 200,
      width: 400,
      legend: 'none',
      titleY: 'Elevation (m)'
    });
  }
}
