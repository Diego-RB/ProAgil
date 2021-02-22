import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Evento } from '../_models/Evento';
import { EventoService } from '../_services/evento.service';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { ToastrService } from 'ngx-toastr';


defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {
  eventosFiltrados: Evento[];
  eventos: Evento[];
  evento: Evento;
  modalRef: BsModalRef;
  registerForm: FormGroup;
  dataEvento: string;


  titulo = 'Eventos';
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  bodyDeletarEvento = '';

  file: File;

  modoSalvar = 'post';
  _filtroLista = '';
  fileNameToUpdate: string;
  dataAtual: string;

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private toastr: ToastrService

    ) {
      this.localeService.use('pt-br');
      }

    get filtroLista(): string{
      return this._filtroLista;
    }
    set filtroLista(value: string){
      this._filtroLista = value;
      this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
    }

    openModal(template: any) {
      this.registerForm.reset();
      this.registerForm.get('dataEvento').setValue(new Date());
      template.show();
    }


    ngOnInit() {
      this.validation();
      this.getEventos();
    }

    filtrarEventos(filtrarPor: string): Evento[] {
      filtrarPor = filtrarPor.toLocaleLowerCase();
      return this.eventos.filter(
        evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
        );
      }

      alternarImagem(){
        this.mostrarImagem = !this.mostrarImagem;
      }

      validation(){
        this.registerForm = this.fb.group({
          tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
          local: ['', Validators.required],
          dataEvento: ['', Validators.required],
          imagemURL: ['', Validators.required],
          qtdPessoas: ['', [Validators.required, Validators.maxLength(12000)]],
          telefone: ['', Validators.required],
          email: ['', [Validators.required, Validators.email]],
          descricao: ['', Validators.required]
        });
      }


      novoEvento(template: any){
        this.modoSalvar = 'post';
        this.openModal(template);
      }

      onFileChange(event) {
        const reader = new FileReader();

        if (event.target.files && event.target.files.length) {
            this.file = event.target.files;
        }
      }

      uploadImagem() {
        if (this.modoSalvar === 'post'){
          const nomeArquivo = this.evento.imagemURL.split('\\', 3);
          this.evento.imagemURL = nomeArquivo[2];
          this.eventoService.postUpload(this.file, nomeArquivo[2])
          .subscribe(
            () => {
              this.dataAtual = new Date().getMilliseconds().toString();
              this.getEventos();
            }
          );
        } else {
          this.evento.imagemURL = this.fileNameToUpdate;
          this.eventoService.postUpload(this.file, this.fileNameToUpdate)
          .subscribe(
            () => {
              this.dataAtual = new Date().getMilliseconds().toString();
              this.getEventos();
            }
          );
        }
      }

      salvarAlteracao(template: any) {
        if (this.registerForm.valid) {
            this.evento = Object.assign({}, this.registerForm.value);

            this.uploadImagem();

            this.eventoService.postEvento(this.evento).subscribe(
              (novoEvento: Evento) => {
                template.hide();
                this.getEventos();
                this.toastr.success('Inserido com Sucesso!');
              }, error => {
                this.toastr.error(`Erro ao Inserir: ${error}`);
              }
            );

        }
      }

          excluirEvento(evento: Evento, template: any) {
            this.openModal(template);
            this.evento = evento;
            this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}, Código: ${evento.id}`;
          }

          confirmeDelete(template: any) {
            this.eventoService.deleteEvento(this.evento.id).subscribe(
              () => {
                template.hide();
                this.getEventos();
                this.toastr.success('Deletado com Sucesso');
              }, error => {
                this.toastr.error('Erro ao tentar deletar');
                console.log(error);
              }
            );
          }

          getEventos() {
            this.dataAtual = new Date().getMilliseconds().toString();
            this.eventoService.getAllEvento().subscribe(
              (_eventos: Evento[]) => {
                this.eventos = _eventos;
                this.eventosFiltrados = this.eventos;
              }, error => {
                this.toastr.error(`Erro ao tentar carregar Eventos: ${error}`);
              });
            }
          }

