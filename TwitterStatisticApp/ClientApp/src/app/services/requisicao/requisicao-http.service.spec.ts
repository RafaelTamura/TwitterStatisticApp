import { TestBed, inject } from '@angular/core/testing';

import { RequisicaoHttpService } from './requisicao-http.service';

describe('RequisicaoHttpService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RequisicaoHttpService]
    });
  });

  it('should be created', inject([RequisicaoHttpService], (service: RequisicaoHttpService) => {
    expect(service).toBeTruthy();
  }));
});
