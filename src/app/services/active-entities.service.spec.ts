import { TestBed } from '@angular/core/testing';

import { ActiveEntitiesService } from './active-entities.service';

describe('ActiveEntitiesService', () => {
  let service: ActiveEntitiesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ActiveEntitiesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
