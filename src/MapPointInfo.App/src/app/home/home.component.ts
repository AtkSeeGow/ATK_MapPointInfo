import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { GoogleMap, GoogleMapsModule, MapMarker } from '@angular/google-maps'
import { BrowserModule } from '@angular/platform-browser';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [GoogleMapsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements AfterViewInit {
  @ViewChild(GoogleMap, { static: false }) map: GoogleMap | undefined

  marker: any = {
    position: {
      lat: 23.97565,
      lng: 120.9738819
    },
    label: {
      color: 'red'
    },
    title: 'asdasd title',
    
    options: { animation: google.maps.Animation.DROP },
  };

  constructor() {
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
