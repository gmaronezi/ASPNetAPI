/* tslint:disable:no-unused-variable */

import { TestBed, async } from '@angular/core/testing';
import { DateTimeFormatPipePipe } from './DateTimeFormatPipe.pipe';
import { Constants } from '../util/Constants';

describe('Pipe: DateTimeFormatPipee', () => {
  it('create an instance', () => {
    let pipe = new DateTimeFormatPipePipe(Constants.DATE_FMT);
    expect(pipe).toBeTruthy();
  });
});
