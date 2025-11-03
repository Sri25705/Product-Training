import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Liveorders } from './liveorders';

describe('Liveorders', () => {
  let component: Liveorders;
  let fixture: ComponentFixture<Liveorders>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Liveorders]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Liveorders);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
