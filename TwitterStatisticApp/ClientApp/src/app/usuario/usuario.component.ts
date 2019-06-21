import { Component, Inject } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html'
})
export class UsuarioComponent {
  usuariosMaisSeguidores: any;

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
    this.obterUsuariosMaisSeguidores();
  }

  private obterUsuariosMaisSeguidores() {
    this.http.get(this.baseUrl + 'api/Twitter/Estatistica/UsuariosMaisSeguidores').subscribe((res) => {
      this.usuariosMaisSeguidores = res;
    });
  }
}
