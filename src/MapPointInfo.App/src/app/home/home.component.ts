import { AfterViewInit, Component, ElementRef, ViewChild } from '@angular/core';



@Component({
  selector: 'app-home',
  standalone: true,
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements AfterViewInit {
  @ViewChild('yourIframe', { static: false }) yourIframe!: ElementRef;

  constructor() {

  }

  ngAfterViewInit() {

  }

  aa() {
    // 確認 yourIframe 不為 undefined
    if (this.yourIframe && this.yourIframe.nativeElement) {
      const iframeElement = this.yourIframe.nativeElement;
      const iframeDocument = iframeElement.contentDocument || iframeElement.contentWindow.document;
        const htmlElement = iframeDocument.querySelector('html');
        htmlElement.setAttribute('data-bs-theme', 'dark');
    }
  }
}
