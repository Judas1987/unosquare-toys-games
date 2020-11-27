import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
  selector: 'app-error-display',
  templateUrl: './error-display.component.html',
  styleUrls: ['./error-display.component.sass']
})
export class ErrorDisplayComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: { errorMessage: string}) { }

  ngOnInit(): void {
  }

}
