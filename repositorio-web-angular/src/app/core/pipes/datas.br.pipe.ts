import { Pipe, PipeTransform } from '@angular/core';
import { formatDate } from '@angular/common';

@Pipe({
    name: 'dataBr',
    standalone: true
})
export class DataBrPipe implements PipeTransform {
    transform(value: Date): string {
        if (!value) return '';

        try {
            return formatDate(value, 'dd/MM/yyyy HH:mm', 'pt-BR');
        } catch (ex) {
            return '';
        }
    }
}
