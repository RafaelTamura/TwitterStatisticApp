import { Component, Inject } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-tweet-hora',
  templateUrl: './tweet-hora.component.html'
})
export class TweetHoraComponent {
  tweetsPorHora: any;

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
    this.obterTweetsPorHora();
  }

  public obterTweetsPorHora() {
    this.http.get(this.baseUrl + 'api/Twitter/Estatistica/TweetsPorHora').subscribe((res) => {
      this.tweetsPorHora = res;
    });
  }
}
