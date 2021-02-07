import { TestBed } from '@angular/core/testing';

import { PorsemanService } from './porseman.service';

describe('PorsemanService', () => {
  let service: PorsemanService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PorsemanService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
