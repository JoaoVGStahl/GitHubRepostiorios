import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ThemeService {
    private isDarkTheme = new BehaviorSubject<boolean>(true);

    constructor() {
        this.aplicarTema(true);
    }

    get temaEscuro$() {
        return this.isDarkTheme.asObservable();
    }

    alternarTema(): void {
        const newTheme = !this.isDarkTheme.value;
        this.isDarkTheme.next(newTheme);
        this.aplicarTema(newTheme);
    }

    private aplicarTema(isDark: boolean): void {
        document.documentElement.setAttribute('data-theme', isDark ? 'dark' : 'light');
    }
} 