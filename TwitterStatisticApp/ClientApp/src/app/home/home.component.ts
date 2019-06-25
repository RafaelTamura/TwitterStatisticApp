import { Component, Inject, ViewChild, ElementRef } from '@angular/core';
import { Login } from '../models/login';
import { environment } from 'src/environments/environment';
import { LocalStorageService } from '../services/local-storage/local-storage.service';
import { RequisicaoHttpService } from '../services/requisicao/requisicao-http.service';
import { HttpResponse } from '@angular/common/http';

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
  login: Login;
  accessToken: string;

  constructor(private localStorage: LocalStorageService,
              private requisicao: RequisicaoHttpService,
              @Inject('BASE_URL') public baseUrl: string) {
    this.textoBotao = 'Carregar';
    this.login = new Login(environment.admin, environment.passwordAdmin);
    this.requisicao.onPost(this.baseUrl + 'api/Login/Acessar', this.login).subscribe((res: HttpResponse<any>) => {
      this.accessToken = res.body;

      if (this.accessToken != null) {
        this.localStorage.setOnLocalStorage(environment.sessionKey, this.accessToken);
      }
    },
    (err) => {
      console.log(err);
    });
  }

  public carregarTweets() {
    this.disableButton = true;
    this.textoBotao = 'Carregando...';
    this.requisicao.onPost(this.baseUrl + 'api/Twitter/Carregar', null).subscribe(() => {
      this.disableButton = false;
      this.textoBotao = 'Carregar';
    });
  }
}
