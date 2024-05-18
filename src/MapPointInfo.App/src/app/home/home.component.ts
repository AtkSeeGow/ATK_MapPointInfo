import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { GoogleMap, GoogleMapsModule, MapAdvancedMarker, MapInfoWindow, MapMarker } from '@angular/google-maps'
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SafePipe } from '../pipes/safe.pipe';
import { InfoWindowOptions, MapOptions } from './home.options';
import { forkJoin } from 'rxjs';

declare let $:any;

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
    forkJoin({
      markers : this.http.get<any>('assets/data/markers.json'),
      markerInfos: this.http.get<any>('assets/data/markerInfos.json')
    }).subscribe(async data => {
      const {PinElement} = await google.maps.importLibrary("marker") as google.maps.MarkerLibrary

      data.markerInfos.forEach((item: any) => {
        this.markerInfos.push(item);
      });

      data.markers.forEach((item: any) => {
        var markerInfos = this.markerInfos.filter(markerInfo => item.title == markerInfo.title);

        var background = "red";
        if(markerInfos.length == 0)
          background = "yellow"

        item.content =  new PinElement({
          background: background,
        }).element;
        this.markers.push(item)
      });

      const centerControlDiv = document.createElement("div");
      const centerControl = this.createCenterControl();
      centerControlDiv.appendChild(centerControl);
      this.map?.controls[google.maps.ControlPosition.RIGHT_TOP].push(centerControlDiv);

      const portalDiv = document.getElementById('myModal')!;
      this.map?.controls[google.maps.ControlPosition.LEFT_TOP].push(portalDiv);
    });
  }

  ngAfterViewInit() {
  }

  getSelectedMarkerInfos(mapAdvancedMarker: any) {
    this.selectedMarkerInfos = this.markerInfos.filter(item => mapAdvancedMarker._title == item.title);

    if(this.selectedMarkerInfos.length == 0)
      this.selectedMarkerInfos[0] = { "dateTime": "9999/12/31 23:59:59", "remark": "資料準備中..." };
    
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

  createCenterControl() {
    const controlButton = document.createElement("button");
  
    // Set CSS for the control.
    controlButton.style.backgroundColor = "#fff";
    controlButton.style.border = "2px solid #fff";
    controlButton.style.borderRadius = "3px";
    controlButton.style.boxShadow = "0 2px 6px rgba(0,0,0,.3)";
    controlButton.style.color = "rgb(25,25,25)";
    controlButton.style.cursor = "pointer";
    controlButton.style.fontFamily = "Roboto,Arial,sans-serif";
    controlButton.style.fontSize = "16px";
    controlButton.style.lineHeight = "38px";
    controlButton.style.margin = "8px 0 22px";
    controlButton.style.padding = "0 5px";
    controlButton.style.textAlign = "center";
    controlButton.textContent = "Center Map";
    controlButton.title = "Click to recenter the map";
    controlButton.type = "button";
    // Setup the click event listeners: simply set the map to Chicago.
    controlButton.addEventListener("click", () => {
      $('#myModal').modal('show'); 
    });
    return controlButton;
  }


}
