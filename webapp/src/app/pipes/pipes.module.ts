import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { FormatSizeUnitPipe } from './format-size-unit.pipe';
import { TruncatePipe } from './truncate.pipe';
import { IconBooleanPipe } from './icon-boolean.pipe';

@NgModule({
    declarations: [
        FormatSizeUnitPipe,
        TruncatePipe,
        IconBooleanPipe,
    ],
    exports: [
        IconBooleanPipe
    ],
    imports: [
        CommonModule,
    ]
})
export class PipesModule { }
