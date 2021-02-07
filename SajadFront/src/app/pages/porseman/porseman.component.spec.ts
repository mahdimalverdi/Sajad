import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PorsemanComponent } from './porseman.component';

describe('PorsemanComponent', () => {
  let component: PorsemanComponent;
  let fixture: ComponentFixture<PorsemanComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PorsemanComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PorsemanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
