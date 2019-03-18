import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-trail',
  templateUrl: './trail.component.html',
  styleUrls: ['./trail.component.scss']
})
export class TrailComponent implements OnInit {
  lat = 51.678418;
  lng = 7.809007;
  origin;
  destination;
  waypoints = [];
  selectedMarker;
  markers = [];

  public renderOptions = {
    draggable: true,
  };

  public change(event: any) {
    this.waypoints = event.request.waypoints;
  }

  constructor() { }

  ngOnInit() {
    this.getDirection();
  }

  addMarker(lat: number, lng: number) {
    this.markers.push({ lat, lng, alpha: 1 });
  }

  max(coordType: 'lat' | 'lng'): number {
    return Math.max(...this.markers.map(marker => marker[coordType]));
  }

  min(coordType: 'lat' | 'lng'): number {
    return Math.min(...this.markers.map(marker => marker[coordType]));
  }

  selectMarker(event) {
    this.selectedMarker = {
      lat: event.latitude,
      lng: event.longitude
    };
  }

  addOrigin(event) {
    this.origin = {
      lat: event.latitude,
      lng: event.longitude
    };
  }

  addDestination(event) {
    this.destination = {
      lat: event.latitude,
      lng: event.longitude
    };
  }

  addWaypoint(event) {
    this.waypoints.push(event.latitude, event.longitude, 1);
  }

  getDirection() {
    this.origin = { lat: 24.799448, lng: 120.979021 };
    this.destination = { lat: 24.799524, lng: 120.975017 };

    // this.origin = 'Taipei Main Station'
    // this.destination = 'Taiwan Presidential Office'
  }

}
