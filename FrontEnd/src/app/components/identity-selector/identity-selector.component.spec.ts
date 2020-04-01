import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IdentitySelectorComponent } from './identity-selector.component';

describe('IdentitySelectorComponent', () => {
  let component: IdentitySelectorComponent;
  let fixture: ComponentFixture<IdentitySelectorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IdentitySelectorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IdentitySelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
