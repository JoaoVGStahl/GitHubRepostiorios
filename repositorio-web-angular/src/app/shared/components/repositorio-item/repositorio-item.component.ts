import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { RepositorioDTO } from '../../../domain/models/repositorio.model';
import { FavoritosService } from '../../../application/services/favoritos.services';
import { CommonModule } from '@angular/common';
import { DataBrPipe } from "../../../core/pipes/datas.br.pipe";
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-repositorio-item',
  imports: [CommonModule, DataBrPipe],
  templateUrl: './repositorio-item.component.html',
  styleUrl: './repositorio-item.component.css'
})
export class RepositorioItemComponent implements OnInit {
  @Input({ required: true }) repositorio!: RepositorioDTO;
  @Output() onRemovidoFavoritos = new EventEmitter<RepositorioDTO>();

  urlSegura!: SafeResourceUrl;

  constructor(private favoritoService: FavoritosService, private sanitizer: DomSanitizer) {

  }

  ngOnInit() {
    this.urlSegura = this.sanitizer.bypassSecurityTrustUrl(this.repositorio.url);
  }

  toogleFavorito(): void {
    if (this.repositorio.favorito) {
      this.removerFavorito();
      return;
    }

    this.adicionarFavorito();
  }

  removerFavorito(): void {
    this.favoritoService.remover(this.repositorio.id)
      .subscribe({
        next: (res) => {
          this.repositorio.favorito = false;
          this.onRemovidoFavoritos.emit(this.repositorio);
        }
      });
  }

  adicionarFavorito(): void {
    this.favoritoService.adicionar(this.repositorio)
      .subscribe({
        next: (res) => {
          this.repositorio.favorito = true;
        }
      });
  }
}
