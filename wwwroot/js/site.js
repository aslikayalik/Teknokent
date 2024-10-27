// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/**arama motoru*/
document.getElementById("search").addEventListener("input", function () {
    var searchTerm = this.value.trim().toLowerCase();
    var rows = document.querySelectorAll("tbody tr");

    rows.forEach(function (row) {
        var studentName = row.children[0].textContent.toLowerCase();
        var studentLastName = row.children[1].textContent.toLowerCase();

        if (studentName.includes(searchTerm) || studentLastName.includes(searchTerm)) {
            row.style.display = "table-row";
        } else {
            row.style.display = "none";
        }
    });
});

document.getElementById("searchButton").addEventListener("click", function () {
  
    event.preventDefault();
    resetRowDisplay();
});

function resetRowDisplay() {
    var rows = document.querySelectorAll("tbody tr");
    rows.forEach(function (row) {
        row.style.display = "table-row";
    });
}



function capitalizeFirstWord(input) {
    var words = input.value.split(' '); 
    for (var i = 0; i < words.length; i++) {
        if (words[i]) {
            words[i] = words[i].charAt(0).toUpperCase() + words[i].slice(1).toLowerCase();
        }
    }
    input.value = words.join(' '); 
}


function toUpperCase(input) {
    input.value = input.value.toUpperCase();
}


//

// Function to increment counters
function incrementCounter(counterId, targetValue, incrementAmount) {
	var current = 0;
	var timer = setInterval(function () {
		current += incrementAmount;
		document.getElementById(counterId).textContent = current;
		if (current >= targetValue) {
			clearInterval(timer);
			document.getElementById(counterId).textContent = targetValue;
		}
	}, 10);
}

// Function to start incrementing counters when scrolled into view
function startCountersWhenInView() {
	var counters = document.querySelectorAll('.counter');
	var options = {
		threshold: 0.1 // Change this threshold as needed, controls when the function gets called
	};

	var observer = new IntersectionObserver(function (entries, observer) {
		entries.forEach(function (entry) {
			if (entry.isIntersecting) {
				var counterId = entry.target.id;
				var targetValue = parseInt(entry.target.dataset.target);
				var incrementAmount = parseInt(entry.target.dataset.increment);
				incrementCounter(counterId, targetValue, incrementAmount);
				observer.unobserve(entry.target);
			}
		});
	}, options);

	counters.forEach(function (counter) {
		observer.observe(counter);
	});
}









// Call the function to start incrementing counters when the page is loaded
window.onload = function () {
	startCountersWhenInView();
};

//	let slideIndex = 0;
const slides = document.querySelectorAll('.slide');
const totalSlides = slides.length;
const slidesToShow = 3; // Ekranda gösterilecek slayt sayısı

function showSlides() {
	// Tüm slaytları gizle
	slides.forEach(slide => {
		slide.style.display = 'none';
	});

	// Gösterilecek slaytları belirle ve görünür yap
	for (let i = 0; i < slidesToShow; i++) {
		let index = (slideIndex + i) % totalSlides;
		slides[index].style.display = 'block';
	}
}

function nextSlide() {
	slideIndex = (slideIndex + 1) % totalSlides;
	showSlides();
}

function prevSlide() {
	slideIndex = (slideIndex - 1 + totalSlides) % totalSlides;
	showSlides();
}

function autoSlide() {
	nextSlide();
}

setInterval(autoSlide, 3000); // Otomatik geçiş için 5 saniyede bir
showSlides(); // Sayfa yüklendiğinde slaytları göster









document.addEventListener("DOMContentLoaded", function () {
	var colors = ["#FF5733", "#33FF57", "#338DFF", "#FF33E6", "#FFD933"];

	var sectors = ["Yazılım", "Hizmet", "Teknoloji Donanımı", "Elektronik", "Telekomünikasyon"];
	var percentages = [20, 15, 10, 25, 30];

	for (var i = 0; i < sectors.length; i++) {
		var chartId = sectors[i].replace(/\s+/g, '-').toLowerCase() + 'Chart';
		createChart(chartId, sectors[i], percentages[i], colors[i]);
	}
});

function createChart(chartId, label, percentage, color) {
	var ctx = document.getElementById(chartId).getContext('2d');
	var chart = new Chart(ctx, {
		type: 'doughnut',
		data: {
			labels: [label, 'Diğer'],
			datasets: [{
				data: [percentage, 100 - percentage],
				backgroundColor: [color, '#eee'],
				borderWidth: 0
			}]
		},
		options: {
			cutoutPercentage: 75,
			rotation: -0.5 * Math.PI,
			circumference: 2 * Math.PI,
			animation: {
				animateRotate: true,
				animateScale: false
			},
			legend: {
				display: false
			}
		}
	});
}