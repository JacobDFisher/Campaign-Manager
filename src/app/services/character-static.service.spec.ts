import { TestBed } from '@angular/core/testing';

import { CharacterStaticService } from './character-static.service';

describe('CharacterStaticService', () => {
  let service: CharacterStaticService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CharacterStaticService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
