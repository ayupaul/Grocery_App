import { TestBed } from '@angular/core/testing';

import { ActionsGuard } from './actions.guard';

describe('ActionsGuard', () => {
  let guard: ActionsGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(ActionsGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
