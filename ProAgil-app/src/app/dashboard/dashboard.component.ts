import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Evento } from '../_models/Evento';
import { User } from '../_models/User';
import { EventoService } from '../_services/evento.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  titulo = 'Pagina Inicial';
  eventos: Evento[];
  evento: Evento;
  user: User = new User();
  registerForm: FormGroup;
  dataAtual: string;
  imagemLargura = 100;
  imagemMargem = 0;
  modoSalvar = 'post';
  mostrarImagem = false;


  constructor(
    private eventoService: EventoService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  ngOnInit() {
    this.getEventos();
  }

  getEventos() {
    this.dataAtual = new Date().getMilliseconds().toString();
    this.eventoService.getAllEvento().subscribe(
      (_eventos: Evento[]) => {
        this.eventos = _eventos;
      }, error => {
        this.toastr.error(`Erro ao tentar carregar Eventos: ${error}`);
      });
    }

    logout() {
      localStorage.removeItem('token');
      this.toastr.show('Log Out');
      this.router.navigate(['/user/login']);
    }

    addFavorito() {
      this.mostrarImagem = !this.mostrarImagem;
    }

}
