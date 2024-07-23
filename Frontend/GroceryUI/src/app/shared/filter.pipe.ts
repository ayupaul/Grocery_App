import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter',
})
export class FilterPipe implements PipeTransform {
  transform(value: any, filter: string, propertyName: string) {
    const result: any = [];
    if (!value || filter === '' || propertyName === '') {
      return value;
    }
    value.forEach((a: any) => {
      if (a[propertyName].trim().toLowerCase().includes(filter.toLowerCase())) {
        return result.push(a);
      }
    });
    return result;
  }
}
