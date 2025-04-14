import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AppRoutes } from '../../domain/consts/routes.const';
import { ThemeToggleComponent } from '../../shared/components/theme-toggle/theme-toggle.component';

@Component({
  selector: 'home-page',
  standalone: true,
  imports: [CommonModule, FormsModule, ThemeToggleComponent],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  constructor(private router: Router) { }

  navegarPara(rota: string): void {
    this.router.navigate([rota]);
  }

  navegarParaFavoritos(): void {
    this.navegarPara(AppRoutes.favoritos);
  }

  navegarParaRepositorios(): void {
    this.navegarPara(AppRoutes.repositorios);
  }

  navegarParaRepositoriosRelevantes(): void {
    this.navegarPara(AppRoutes.repositoriosRelevantes);
  }

  navegarParaRepositoriosUsuario(): void {
    this.navegarPara(AppRoutes.repositoriosUsuario);
  }
}