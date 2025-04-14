import { Injectable, PLATFORM_ID, Inject } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { isPlatformBrowser } from '@angular/common';

@Injectable({
    providedIn: 'root'
})
export class ThemeService {
    private isDarkTheme = new BehaviorSubject<boolean>(true);
    private isBrowser: boolean;

    constructor(@Inject(PLATFORM_ID) platformId: Object) {
        this.isBrowser = isPlatformBrowser(platformId);
        if (this.isBrowser) {
            this.aplicarTema(true);
        }
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
        if (this.isBrowser) {
            document.documentElement.setAttribute('data-theme', isDark ? 'dark' : 'light');
        }
    }
} 