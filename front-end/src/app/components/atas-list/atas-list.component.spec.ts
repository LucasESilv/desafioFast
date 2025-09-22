import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AtasListComponent } from './atas-list.component';

describe('AtasListComponent', () => {
  let component: AtasListComponent;
  let fixture: ComponentFixture<AtasListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AtasListComponent]
    });
    fixture = TestBed.createComponent(AtasListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
