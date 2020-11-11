import {ComponentFixture, TestBed} from '@angular/core/testing';

import {DeleteConfirmationDialogComponent} from './delete-confirmation-dialog.component';
import {MatDialogModule, MatDialogRef} from '@angular/material/dialog';

describe('DeleteConfirmationDialogComponent', () => {
    let component: DeleteConfirmationDialogComponent;
    let fixture: ComponentFixture<DeleteConfirmationDialogComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [MatDialogModule, MatDialogRef],
            declarations: [DeleteConfirmationDialogComponent]
        })
            .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(DeleteConfirmationDialogComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
