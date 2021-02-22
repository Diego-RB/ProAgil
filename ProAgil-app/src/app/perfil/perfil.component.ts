import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Evento } from '../_models/Evento';
import { User } from '../_models/User';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {


    registerForm: FormGroup;

    user: User = new User();
    users: User[];
    imagemURL = 'assets/img/upload.png';
    evento: Evento = new Evento();
    fileNameToUpdate: string;
    dataAtual = '';
    file: File;
    name: any;

  constructor(
    private fb: FormBuilder,
    private router: ActivatedRoute,
    private userService:UserService,
    private toastr: ToastrService
  ) { }

  ngOnInit() {


  }




}