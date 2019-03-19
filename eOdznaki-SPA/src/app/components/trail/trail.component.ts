import {Component, OnInit} from '@angular/core';
import {google} from '@agm/core/services/google-maps-types';

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
  waypoints: any = [];
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
  }

  addMarker(lat: number, lng: number) {
    const latitude = lat;
    const longitude = lng;
    if (this.origin == null) {
      this.origin = {
        lat: latitude,
        lng: longitude
      };
    } else if (this.destination == null) {
      this.destination = {
        lat: latitude,
        lng: longitude
      };
    } else {
      let oldLat = this.destination.lat;
      let oldLng = this.destination.lng;
      let newLocation = new google.maps.LatLng(oldLat, oldLng);
        this.waypoints.push({ location: newLocation, stopover: false });
        this.destination = {
          lat: latitude,
          lng: longitude
        };
      this.markers.push({ lat: latitude, lng: longitude, alpha: 1 });
    }
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

  // openContextMenu(event) {
  //   let menu = new BootstrapMenu('#mapMenu', {
  //     actions: [{
  //       name: 'Dodaj jako punkt startowy',
  //       onClick: this.addOrigin(event)
  //     }, {
  //       name: 'Dodaj jako punkt docelowy',
  //       onClick: this.addDestination(event)
  //     }, {
  //       name: 'Dodaj jako punkt pośredni',
  //       onClick: this.addWaypoint(event)
  //     }]
  //   });
  // }

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
  }

}
