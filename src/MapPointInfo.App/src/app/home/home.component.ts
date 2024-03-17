import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { GoogleMap, GoogleMapsModule, MapMarker } from '@angular/google-maps'
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [GoogleMapsModule, HttpClientModule, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements AfterViewInit {
  @ViewChild(GoogleMap, { static: false }) map: GoogleMap | undefined

  markers: any[] = [];

  constructor(private http: HttpClient) {
    this.http.get<any>('assets/data/markers.json').subscribe(data => {
      data.forEach((item: any) => {
        this.markers.push(item)
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

  aa(marker: any){
    alert(marker.title)
  }
}
