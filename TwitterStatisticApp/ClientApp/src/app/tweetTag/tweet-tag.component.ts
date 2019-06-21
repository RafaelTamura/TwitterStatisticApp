import { Component, Inject } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-tweet-tag',
  templateUrl: './tweet-tag.component.html'
})
export class TweetTagComponent {
  tweetsPorTag: any;

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
    this.obterTweetsPorTag();
  }

  public obterTweetsPorTag() {
    this.http.get(this.baseUrl + 'api/Twitter/Estatistica/TweetsPorTag').subscribe((res) => {
      this.tweetsPorTag = res;
    });
  }
}
