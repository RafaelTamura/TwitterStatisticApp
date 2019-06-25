import { Component, Inject } from '@angular/core';
import { RequisicaoHttpService } from '../services/requisicao/requisicao-http.service';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-tweet-hora',
  templateUrl: './tweet-hora.component.html'
})
export class TweetHoraComponent {
  tweetsPorHora: any;

  constructor(private requisicao: RequisicaoHttpService, @Inject('BASE_URL') public baseUrl: string) {
    this.obterTweetsPorHora();
  }

  public obterTweetsPorHora() {
    this.requisicao.onGet(this.baseUrl + 'api/Twitter/Estatistica/TweetsPorHora').subscribe((res: HttpResponse<any>) => {
      this.tweetsPorHora = res.body;
    });
  }
}
