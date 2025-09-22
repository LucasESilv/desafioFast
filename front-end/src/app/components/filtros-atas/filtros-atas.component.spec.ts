import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FiltrosAtasComponent } from './filtros-atas.component';

describe('FiltrosAtasComponent', () => {
  let component: FiltrosAtasComponent;
  let fixture: ComponentFixture<FiltrosAtasComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FiltrosAtasComponent]
    });
    fixture = TestBed.createComponent(FiltrosAtasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
