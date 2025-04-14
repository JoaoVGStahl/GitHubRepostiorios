import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ThemeService } from '../../../core/services/theme.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-theme-toggle',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatIconModule, MatTooltipModule],
  template: `
    <button mat-icon-button (click)="toggleTheme()" [matTooltip]="isDark ? 'Tema Claro' : 'Tema Escuro'">
      <mat-icon>{{ isDark ? 'light_mode' : 'dark_mode' }}</mat-icon>
    </button>
  `,
  styles: [`
    :host {
      display: block;
    }
  `]
})
export class ThemeToggleComponent {
  isDark = false;
  temaEscuro$: Observable<boolean>;

  constructor(private themeService: ThemeService) {
    this.temaEscuro$ = this.themeService.temaEscuro$;
    this.themeService.temaEscuro$.subscribe((isDark: boolean) => {
      this.isDark = isDark;
    });
  }

  toggleTheme(): void {
    this.themeService.alternarTema();
  }
} 