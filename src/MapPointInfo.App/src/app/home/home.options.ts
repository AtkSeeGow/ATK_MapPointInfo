export class MapOptions {
  center: google.maps.LatLngLiteral = {
    lat: 24.921854890757547,
    lng: 121.5102934100463
  };
  zoom = 10;
  mapId = "fcc36b61505ecdf9";
}

export class InfoWindowOptions implements google.maps.InfoWindowOptions {
  maxWidth: number = 1500;
}