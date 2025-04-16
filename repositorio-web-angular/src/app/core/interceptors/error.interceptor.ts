import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { ErrorDialogComponent } from '../../shared/components/error-dialog/error-dialog.component';

const obterMensagemErro = (httpError: any): { title: string; message: string } => {
    const errorMapping: Record<number, string> = {
        400: 'Requisição Inválida',
        401: 'Não Autorizado',
        403: 'Acesso Negado',
        404: 'Recurso Não Encontrado',
        500: 'Erro Interno'
    };

    const title = errorMapping[httpError.status] || 'Erro Inesperado';
    const message = httpError.error?.message || 'Ocorreu um erro inesperado. Tente novamente mais tarde.';

    return { title, message };
};

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
    const dialog = inject(MatDialog);

    return next(req).pipe(
        catchError((error) => {
            const { title, message } = obterMensagemErro(error);

            dialog.open(ErrorDialogComponent, {
                data: { title, message },
                width: '400px',
                disableClose: true,
                panelClass: 'error-dialog-container'
            });

            return throwError(() => error);
        })
    );
}; 