import { CountersSummary, ScoreHistoryItem } from '@/models';
import { DateTime } from 'luxon';

export class ChartService {
	public static colorNoData = '#B7B8A8';
	public static colorFailed = '#E33035';
	public static colorWarning = '#F8A462';
	public static colorSuccess = '#41C6B9';

	public static groupColors = [ChartService.colorNoData, ChartService.colorFailed, ChartService.colorWarning, ChartService.colorSuccess];
	public static successThresholdScore = 75;


	/**
	 * Return severity color for charts.
	 *
	 * @private
	 * @static
	 * @param {string} severity
	 * @returns
	 * @memberof ChartService
	 */
	private static getSeverityColor(severity: string) {
		switch (severity) {
			case 'CRITICAL':
				return ChartService.groupColors[1];
			case 'MEDIUM':
				return ChartService.groupColors[2];
			case 'NOISSUES':
				return ChartService.groupColors[3];
			case 'NODATA':
				return ChartService.groupColors[0];
		}
	}

	/**
	 * Get color for score text.
	 *
	 * @private
	 * @static
	 * @param {number} score
	 * @returns
	 * @memberof ChartService
	 */
	private static getColorByScore(score: number) {
		if (score>ChartService.successThresholdScore) return ChartService.colorSuccess;
		if (score>50) return ChartService.colorWarning;
		return ChartService.colorFailed;
	}

	/**
	 * Anniotation for threshold on area charts.
	 *
	 * @readonly
	 * @private
	 * @static
	 * @memberof ChartService
	 */
	private static get thresholdAnnotation() {
		return [{
			y: ChartService.successThresholdScore,
			y2: null,
			strokeDashArray: 3,
			borderColor: '#ccc',
			fillColor: '#d2d2d2',
			opacity: 0.3,
			offsetX: 0,
			yAxisIndex: 0,
			label: {
				borderWidth: 0,
				text: ChartService.successThresholdScore + '%',
				textAnchor: 'start',
				position: 'left',
				offsetY:6,
				offsetX:120,
				style: {
					background: '#ddd', //transparent',
					fontSize: '8px',
					color:'#888',
					padding: {
						left:0,
						right:0,
						top:0,
						bottom:0
					}
				}
			}
		}]
	}

	/**
	 * Enable animation in chart.
	 *
	 * @readonly
	 * @private
	 * @static
	 * @memberof ChartService
	 */
	private static get animationOptions() {
		return {
			enabled: true,
			easing: 'linear',
			speed: 10,
			dynamicAnimation: {
				enabled: true,
				speed: 150
			}
		}
	}

	/**
	 * Disable animation in chart.
	 *
	 * @readonly
	 * @private
	 * @static
	 * @memberof ChartService
	 */
	private static get noAnimation() {
		return {
			enabled: false
		}
	}

	/**
	 * Return area chart options.
	 *
	 * @static
	 * @param {string} id
	 * @param {ScoreHistoryItem[]} scoreHistory
	 * @param {DateTime[]} dates
	 * @param {number[]} scores
	 * @param {Function} cb
	 * @returns {ApexCharts.ApexOptions}
	 * @memberof ChartService
	 */
	public static AreaChartOptions(id:string,scoreHistory: ScoreHistoryItem[], dates: DateTime[], scores: number[], cb: Function) : ApexCharts.ApexOptions {

		const xAxisAnnotations: any[] = [];
		xAxisAnnotations.push({
			x: new Date(dates[0].toISODate()).getTime(),
			x2: (dates.length === 2 && scores.length === 2) ? new Date(dates[1].toISODate()).getTime() : null,
			offsetX:-1,
			strokeDashArray: 0,
			borderWidth: 1,
			borderColor: '#F8A462',
			label: {
				borderWidth:0,
				text: `${scores[0]} %`,
				offsetX:-1,
				style: {
					background: '#F8A462', //transparent',
					fontSize: '7px',
					color:'#fff',
					padding: {
						left:3,
						right:3,
						top:1,
						bottom:0
					}
				}
			},
		})
		if (dates.length === 2 && scores.length === 2) {
			xAxisAnnotations.push({
				x: new Date(dates[1].toISODate()).getTime(),
				offsetX:-1,
				borderWidth: 1,
				borderColor: '#F8A462',
				strokeDashArray: 0,
				label: {
					borderWidth:0,
					text: `${scores[1]} %`,
					offsetX:-1,
					style: {
						background: '#F8A462', //transparent',
						fontSize: '7px',
						color:'#fff',
						padding: {
							left:3,
							right:3,
							top:1,
							bottom:0
						}
					}	
				},
			})	
		}

		return <ApexCharts.ApexOptions>{
			chart: {
				id: id,
				type: 'area',
				sparkline: { enabled: true },
				events: {
					markerClick: function(event, chartContext, { seriesIndex, dataPointIndex, config}) {
						const index = scoreHistory.length - dataPointIndex - 1;
						const datestr = scoreHistory[index].recordedAt.split('T')[0];
						scores[0] = scoreHistory[index].score;
						if(scores[0]>0) {
							// dont call on score=0
							cb(datestr, scores[0]);						
						}
					}
				},
				animations: ChartService.noAnimation
			},
			colors: [ChartService.colorSuccess, ChartService.colorWarning, ChartService.colorFailed],
			stroke: { width: 1 },
			yaxis: {
				type: 'numeric',
				labels: {
					formatter: (value) => { return value + "%" },
					show: false,
				}								
			},
			xaxis: {
				type: 'datetime',
				crosshairs: { width: 1 }
			},
			tooltip: {
				fixed: {
					enabled: false
				},
				marker: {
					show: false
				},
				custom: function({series, seriesIndex, dataPointIndex, w}) {
					const dateStr = scoreHistory[scoreHistory.length -1 - dataPointIndex].recordedAt.split('T')[0];
					const scoreStr = series[seriesIndex][dataPointIndex] + '%';

					return '<div style="border-radius:2px;padding:2px;font-size:9px;">' +
					'<span style="color:#666">' + dateStr + '</span>' +
					'<br>Score: <b>' + scoreStr + '</b>' +
					'</div>'
				}
			},
			annotations: {
				xaxis: xAxisAnnotations,
				yaxis: ChartService.thresholdAnnotation,
			}
		}

	}

	/**
	 * Return Donut chart options.
	 *
	 * @static
	 * @param {string} id
	 * @param {CountersSummary} [summary]
	 * @param {Function} [cb]
	 * @returns {ApexCharts.ApexOptions}
	 * @memberof ChartService
	 */
	public static DonutChartOptions(id:string, summary?: CountersSummary, cb?: Function) : ApexCharts.ApexOptions {

			return <ApexCharts.ApexOptions>{
				chart: {
					type: 'donut',
					sparkline: {
						enabled: true
					},
					dropShadow: {
						enabled: true,
						top: 0,
						left: 0,
						blur: 2,
						opacity: 0.1
					},
					events: {
						dataPointSelection: function name(event, chartContext, config) {
							if(cb && summary) {
								let index = config.dataPointIndex;
								let value = summary.getLabels()[index].replace(" ", "");
								cb(value);	
							}
						}
					},
					animations: ChartService.animationOptions
				},
				labels: summary === undefined ? ['a'] : summary.getLabels(),
				colors: summary === undefined ? [ChartService.colorNoData] : summary.getColors(),			  
				stroke: {
					width: 1
				},
				tooltip: {
					fixed: {
						enabled: false
					},
				},
				plotOptions: {
					pie: {
						donut: {
							labels: {
								show: true,
								showAlways: true,
								name: {
									show: true,
									offsetY: 5
								},
								value: {
									show: false
								},
								total: {
									color: summary === undefined ? ChartService.colorNoData : ChartService.getColorByScore(summary.score),
									show: true,
									showAlways: true,
									label: summary === undefined ? '0' : summary.score + '%',
									fontSize: '13px'
								}
							}
						}
					}
				}
			}
	}

	/**
	 * Returns Piechart options.
	 *
	 * @static
	 * @param {string} id Id of the element
	 * @param {CountersSummary} summary the summary values for the pie chart
	 * @param {Function} cb callback function when clicked on pie
	 * @param {boolean} [small=false] if it is displayed on diff page or not
	 * @returns {ApexCharts.ApexOptions}
	 * @memberof ChartService
	 */
	public static PieChartOptions(id:string, summary: CountersSummary, cb: Function, small:boolean = false) : ApexCharts.ApexOptions {
		
		return <ApexCharts.ApexOptions>{
			chart: {
				type: 'pie',
				offsetY: small ? 10 : -20,
				dropShadow: {
					enabled: small ? false : true,
					top: 10,
					left: 10,
					blur: 10,
					opacity: 0.1
				},
				animations: ChartService.animationOptions,
				events: {
					dataPointSelection: function name(event, chartContext, config) {
						console.log(config);
						let index = config.dataPointIndex;
						let value = summary.getLabels()[index].replace(" ", "");
						cb(value);
					}
				}
			},
			legend: {
				show: small ? false : true,
				offsetY: 10
			},
			labels: summary.getLabels(),
			colors: summary.getColors(),
			plotOptions: {
				pie: {
					expandOnClick: false, 
					offsetX: small ? 0 : 45,
					customScale: small ? 1 : 0.8,
					dataLabels: {
						offset:-10
					}
				},
			},
			stroke: {
				show: true,
				width: 1
			}
		}

	}
}
