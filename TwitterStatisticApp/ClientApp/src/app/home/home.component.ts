import { Component, Inject, ViewChild, ElementRef } from '@angular/core';
import { HttpResponse, HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  @ViewChild('btnCarregar') btnCarregar: ElementRef;

  disableButton = false;
  tweetsPorHora: any;
  tweetsPorTag: any;
  textoBotao: string;

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
    this.textoBotao = 'Carregar';
  }

  public carregarTweets() {
    this.disableButton = true;
    this.textoBotao = 'Carregando...';
    this.http.post(this.baseUrl + 'api/Twitter/Carregar', null).subscribe(() => {
      this.disableButton = false;
      this.textoBotao = 'Carregar';
    });
  }
}
