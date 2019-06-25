import { Component, Inject } from '@angular/core';
import { RequisicaoHttpService } from '../services/requisicao/requisicao-http.service';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-tweet-tag',
  templateUrl: './tweet-tag.component.html'
})
export class TweetTagComponent {
  tweetsPorTag: any;

  constructor(private requisicao: RequisicaoHttpService, @Inject('BASE_URL') public baseUrl: string) {
    this.obterTweetsPorTag();
  }

  public obterTweetsPorTag() {
    this.requisicao.onGet(this.baseUrl + 'api/Twitter/Estatistica/TweetsPorTag').subscribe((res: HttpResponse<any>) => {
      this.tweetsPorTag = res.body;
    });
  }
}
