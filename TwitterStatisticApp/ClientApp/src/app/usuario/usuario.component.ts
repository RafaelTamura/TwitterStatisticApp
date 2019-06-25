import { Component, Inject } from '@angular/core';
import { RequisicaoHttpService } from '../services/requisicao/requisicao-http.service';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html'
})
export class UsuarioComponent {
  usuariosMaisSeguidores: any;

  constructor(private requisicao: RequisicaoHttpService, @Inject('BASE_URL') public baseUrl: string) {
    this.obterUsuariosMaisSeguidores();
  }

  private obterUsuariosMaisSeguidores() {
    this.requisicao.onGet(this.baseUrl + 'api/Twitter/Estatistica/UsuariosMaisSeguidores').subscribe((res: HttpResponse<any>) => {
      this.usuariosMaisSeguidores = res.body;
    });
  }
}
