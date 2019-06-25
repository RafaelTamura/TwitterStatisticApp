import { Injectable } from 'node_modules/@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from 'node_modules/@angular/common/http';
import { catchError } from 'node_modules/rxjs/operators';
import { Observable, throwError } from 'node_modules/rxjs';
import { LocalStorageService } from '../local-storage/local-storage.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RequisicaoHttpService {

  private headers: HttpHeaders;
  private sessao: any;

  constructor(private http: HttpClient,
              private localStorage: LocalStorageService) {
  }

  private setHeaders() {
    this.sessao = this.localStorage.getOnLocalStorage(environment.sessionKey);

    if (this.sessao !== null && this.sessao) {
      this.headers = new HttpHeaders({'Content-Type': 'application/json; charset=utf-8',
      'Authorization': 'Bearer ' + this.sessao});
    } else {
      this.headers = new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'});
    }
  }

  public onGet(url: string) {
    this.setHeaders();
    return this.http.get<any>(url, { headers: this.headers, observe: 'response' })
             .pipe(
               catchError(this.handleError)
             );
  }

  public onPost(url: string, dados: any): Observable<any> {
    this.setHeaders();
    return this.http.post<any>(url, JSON.stringify(dados), {headers: this.headers, observe: 'response'})
             .pipe(
               catchError(this.handleError)
             );
  }

  private handleError(error: HttpErrorResponse) {
    const errorList: number[] = [304, 400, 401, 412];

    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,

      // Erro tratado pelo serviço
      if (errorList.indexOf(error.status) < 0) {
        console.error(
          'Backend returned code ' + error.status + ', ' +
          'body was: ' + error.message + '.');

        if (error.error !== null) {
          if ('errors' in error.error) {
            return throwError(
              error.error.errors);
          } else {
            return throwError(
              error.error);
          }
        }
      }
    }

    // Erro tratado pelo serviço
    if (errorList.indexOf(error.status) >= 0) {
      if (error.error !== null) {
        if ('errors' in error.error) {
          return throwError(
            error.error.errors);
        } else {
          return throwError(
            error.error);
        }
      }
    }

    // return an observable with a user-facing error message
    return throwError(
      '');
  }
}
