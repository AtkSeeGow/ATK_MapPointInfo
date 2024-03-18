import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { GoogleMap, GoogleMapsModule, MapAdvancedMarker } from '@angular/google-maps'
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [GoogleMapsModule, HttpClientModule, CommonModule, FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements AfterViewInit {
  @ViewChild(GoogleMap, { static: false }) map: GoogleMap | undefined

  markers: any[] = [];
  markerInfos: any[] = [];
  
  selectedMarkerInfos: any[] = [];
  selectedMarkerInfo: any = { "youTubeLink": "https://www.youtube.com/embed/mmujjwPHa10?si=QsNpnU8vost4tP6I"};
  selectedMarkerInfoDateTimes: string[] = [];

  constructor(private http: HttpClient, private sanitizer: DomSanitizer) {
    this.http.get<any>('assets/data/markers.json').subscribe(data => {
      data.forEach((item: any) => {
        this.markers.push(item)
      });
    });

    this.http.get<any>('assets/data/markerInfos.json').subscribe(data => {
      data.forEach((item: any) => {
        this.markerInfos.push(item)
      });
    });
  }

  ngAfterViewInit() {
  }

  center: google.maps.LatLngLiteral = {
    lat: 23.97565,
    lng: 120.9738819,
  };
  zoom = 8;

  getSelectedMarkerInfos(marker: any){
    this.selectedMarkerInfos = this.markerInfos.filter(item => marker.title == item.title);
    this.selectedMarkerInfoDateTimes = []
    this.selectedMarkerInfos.forEach((item: any) => {
      this.selectedMarkerInfoDateTimes.push(item.dateTime)
    });
    this.getSelectedMarkerInfo({ "target": { "value": this.selectedMarkerInfoDateTimes[0] }});
  }

  getSelectedMarkerInfo(event: any)
  {
    const selectedValue = (event.target as HTMLSelectElement).value;
    this.selectedMarkerInfo = this.selectedMarkerInfos.filter(item => item.dateTime == selectedValue)[0];
  }

  bypassSecurityTrustResourceUrl(url: string){
    return this.sanitizer.bypassSecurityTrustResourceUrl(url);
  }
}
