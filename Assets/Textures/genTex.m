## Copyright (C) 2020 小林悠紀
## 
## This program is free software: you can redistribute it and/or modify it
## under the terms of the GNU General Public License as published by
## the Free Software Foundation, either version 3 of the License, or
## (at your option) any later version.
## 
## This program is distributed in the hope that it will be useful, but
## WITHOUT ANY WARRANTY; without even the implied warranty of
## MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
## GNU General Public License for more details.
## 
## You should have received a copy of the GNU General Public License
## along with this program.  If not, see
## <https://www.gnu.org/licenses/>.

## -*- texinfo -*- 
## @deftypefn {} {@var{retval} =} genTex (@var{input1}, @var{input2})
##
## @seealso{}
## @end deftypefn

## Author: 小林悠紀 <yuki@MacBook-Pro.AirPort>
## Created: 2020-10-12

function genTex ()

  genTex2([128, 0, 0], 'blockTextureR.png');
  genTex2([0, 128, 0], 'blockTextureG.png');
  genTex2([0, 0, 128], 'blockTextureB.png');

endfunction

function genTex2 (color, fileName)

  img = zeros(256, 256, 3, 'uint8');

  for (i = 1:3)
    img(:,    1:16, i) = color(i);
    img(:, 241:256, i) = color(i);
    img(   1:16, :, i) = color(i);
    img(241:256, :, i) = color(i);
  endfor

  imwrite(img, fileName);

endfunction
