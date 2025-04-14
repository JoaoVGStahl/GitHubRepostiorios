import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RepositoriosApiProvider } from '../../core/providers/repositorio.api.provider';
import { Router } from '@angular/router';


@Component({
  selector: 'home-page',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  repos: string = '';
  repositorios: any[] = [];

  constructor(private repositorioService: RepositoriosApiProvider, private router: Router) { }

  ngOnInit(): void { }

  buscarRepos(): void {
    if (!this.repos) return;
  }

  navegar(rota: string): void {
    this.router.navigate([rota]);
  }
}