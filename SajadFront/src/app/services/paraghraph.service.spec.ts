import { TestBed } from '@angular/core/testing';

import { ParaghraphService } from './paraghraph.service';

describe('ParaghraphService', () => {
  let service: ParaghraphService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ParaghraphService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
