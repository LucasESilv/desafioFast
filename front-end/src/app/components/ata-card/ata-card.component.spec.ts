import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AtaCardComponent } from './ata-card.component';

describe('AtaCardComponent', () => {
  let component: AtaCardComponent;
  let fixture: ComponentFixture<AtaCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AtaCardComponent]
    });
    fixture = TestBed.createComponent(AtaCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
