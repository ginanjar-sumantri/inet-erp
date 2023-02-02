var SLIDETIMER = 3;
var SLIDESPEED = 3;
var SCROLLTIMER = 3;
var SCROLLSPEED = 3;
var STARTINGOPACITY = 40;

// handles section to section scrolling of the content //
function slideContent(id,prefix,timer) {
  var div = document.getElementById(id);
  var slider = div.parentNode;
  clearInterval(slider.timer);
  slider.section = parseInt(id.replace(/\D/g,''));
  slider.target = div.offsetleft;
  slider.style.left = slider.style.left || '0px';
  slider.current = slider.style.left.replace('px', '');
  slider.direction = (Math.abs(slider.current) > slider.target) ? 1 : -1;
  slider.style.opacity = STARTINGOPACITY * .01;
  slider.style.filter = 'alpha(opacity=' + STARTINGOPACITY + ')';
  slider.timer = setInterval( function() { slideAnimate(slider,prefix,timer) }, SLIDETIMER);
}

function slideAnimate(slider,prefix,timer) {
  var curr = Math.abs(slider.current);
  var tar = Math.abs(slider.target);
  var dir = slider.direction;
  if((tar - curr <= SLIDESPEED && dir == -1) || (curr - tar <= SLIDESPEED && dir == 1)) {
    slider.style.left = (slider.target * -1) + 'px';
	slider.style.opacity = 1;
	slider.style.filter = 'alpha(opacity=100)';
    clearInterval(slider.timer);
	if(slider.autoscroll) {
	  setTimeout( function() { autoScroll(slider.id,prefix,timer) }, timer * 1000);
	}
  } else {
	var pos = (dir == 1) ? parseInt(slider.current) + SLIDESPEED : slider.current - SLIDESPEED;
    slider.current = pos;
    slider.style.left = pos + 'px';
  }
}

// handles manual scrolling of the content //
function scrollContent(id,dir) {
  var div = document.getElementById(id);
  clearInterval(div.timer);
  var sections = div.getElementsByTagName('div');
  var length = sections.length;
  var limit;
  if(dir == -1) {
    limit = 0;
  } else {
    if(length > 1) {
        limit = sections[length - 1].offsetleft;
    } else {
    limit = div.offsetWidth - div.parentNode.offsetWidth + 20;
    }
  }  
  div.style.opacity = STARTINGOPACITY * .01;
  div.style.filter = 'alpha(opacity=' + STARTINGOPACITY + ')';
  div.timer = setInterval( function() { scrollAnimate(div,dir,limit) }, SCROLLTIMER);
}

function scrollAnimate(div,dir,limit) {
  div.style.left = div.style.left || '0px';
  var left = div.style.left.replace('px','');
  if(dir == 1) {
	if(limit - Math.abs(left) <= SCROLLSPEED) {
	  cancelScroll(div.id);
	  div.style.left = '-' + limit + 'px';
	} else {
	  div.style.left = left - SCROLLSPEED + 'px';
	}
  } else {
	if(Math.abs(left) - limit <= SCROLLSPEED) {
	  cancelScroll(div.id);
	  div.style.left = limit + 'px';
	} else {
	  div.style.left = parseInt(left) + SCROLLSPEED + 'px';
	}
  }
}

// cancel the scrolling on mouseout //
function cancelScroll(id) {
  var div = document.getElementById(id);
  div.style.opacity = 1;
  div.style.filter = 'alpha(opacity=100)';
  clearTimeout(div.timer);
}

// initiate auto scrolling //
function autoScroll(id,prefix,timer,restart) {
  var div = document.getElementById(id);
  div.autoscroll = (!div.autoscroll && !restart) ? false : true;
  if(div.autoscroll) {
    var sections = div.getElementsByTagName('div');
    var length = sections.length;
    div.section = (div.section && div.section < length) ? div.section + 1 : 1;
    slideContent(prefix + '-' + div.section,prefix,timer);
  }
}

// cancel automatic scrolling //
function cancelAutoScroll(id) {
  var div = document.getElementById(id);
  div.autoscroll = false;
}