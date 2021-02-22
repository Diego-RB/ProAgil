import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';



@Pipe({
  name: 'DateTimeFormatPipe'
})
export class DateTimeFormatPipePipe extends DatePipe implements PipeTransform {

  transform(date: string): string {
    const dateOut: moment.Moment = moment(date,"DD/MM/YYYY HH:mm:ss");
    return dateOut.format("DD/MM/YYYY HH:mm");
  }

}
