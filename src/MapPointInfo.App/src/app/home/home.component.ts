import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { GoogleMap, GoogleMapsModule, MapAdvancedMarker, MapInfoWindow, MapMarker } from '@angular/google-maps'
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SafePipe } from '../pipes/safe.pipe';
import { InfoWindowOptions, MapOptions } from './home.options';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [GoogleMapsModule, HttpClientModule, CommonModule, FormsModule, SafePipe],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements AfterViewInit {
  @ViewChild(GoogleMap, { static: false }) map: GoogleMap | undefined
  @ViewChild(MapInfoWindow) infoWindow: MapInfoWindow | undefined;

  markers: any[] = [];
  markerInfos: any[] = [];

  selectedMarkerInfos: any[] = [];
  selectedMarkerInfo: any = null;
  selectedMarkerInfoDateTimes: string[] = [];

  mapOptions: MapOptions = new MapOptions();
  infoWindowOptions: InfoWindowOptions = new InfoWindowOptions();

  constructor(private http: HttpClient) {
    this.http.get<any>('assets/data/markers.json').subscribe(data => {
      data.forEach((item: any) => {
        this.markers.push(item)
      });
    });

    this.http.get<any>('assets/data/markerInfos.json').subscribe(data => {
      data.forEach((item: any) => {
        this.markerInfos.push(item);
      });
    });
  }

  ngAfterViewInit() {
  }

  getSelectedMarkerInfos(mapAdvancedMarker: any) {
    this.selectedMarkerInfos = this.markerInfos.filter(item => mapAdvancedMarker._title == item.title);
    this.selectedMarkerInfoDateTimes = []
    this.selectedMarkerInfos.forEach((item: any) => {
      item.lat = mapAdvancedMarker._position.lat;
      item.lng = mapAdvancedMarker._position.lng;
      item.garminLink = "https://connect.garmin.com/modern/activity/embed/" + item.garminConnectId
      this.selectedMarkerInfoDateTimes.push(item.dateTime)
    });
    this.getSelectedMarkerInfo({ "target": { "value": this.selectedMarkerInfoDateTimes[0] } });
    this.infoWindow!.openAdvancedMarkerElement(mapAdvancedMarker.advancedMarker);
  }

  getSelectedMarkerInfo(event: any) {
    const selectedValue = (event.target as HTMLSelectElement).value;
    this.selectedMarkerInfo = this.selectedMarkerInfos.filter(item => item.dateTime == selectedValue)[0];
  }
}
