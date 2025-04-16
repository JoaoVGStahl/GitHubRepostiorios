import { Component } from '@angular/core';
import { RouterModule, Router } from '@angular/router';
import { AppRoutes } from '../../domain/consts/routes.const';

@Component({
  selector: 'app-not-found',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './not-found.component.html',
  styleUrls: ['./not-found.component.css']
})
export class NotFoundComponent {
  constructor(private router: Router) { }

  voltarParaHome(): void {
    this.router.navigate([AppRoutes.home]);
  }
}
