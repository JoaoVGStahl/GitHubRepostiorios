import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { ErrorDialogComponent } from '../../shared/components/error-dialog/error-dialog.component';

const obterMensagemErro = (error: any): { title: string; message: string } => {
    if (error.error instanceof ErrorEvent) {
        return {
            title: 'Erro de Comunicação',
            message: `Erro: ${error.error.message}`
        };
    }

    switch (error.status) {
        case 400:
            return {
                title: 'Requisição Inválida',
                message: 'Verifique os dados enviados e tente novamente.'
            };
        case 401:
            return {
                title: 'Não Autorizado',
                message: 'Faça login novamente para continuar.'
            };
        case 403:
            return {
                title: 'Acesso Negado',
                message: 'Você não tem permissão para realizar esta ação.'
            };
        case 404:
            return {
                title: 'Recurso Não Encontrado',
                message: 'O recurso solicitado não foi encontrado.'
            };
        case 500:
            return {
                title: 'Erro Interno',
                message: 'Ocorreu um erro no servidor. Tente novamente mais tarde.'
            };
        default:
            return {
                title: 'Erro Inesperado',
                message: 'Ocorreu um erro inesperado. Tente novamente mais tarde.'
            };
    }
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