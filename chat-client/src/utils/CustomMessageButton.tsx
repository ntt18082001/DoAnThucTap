import { ButtonUnstyled, ButtonUnstyledProps } from '@mui/base';
import { styled } from '@mui/material';
import * as React from 'react';

const darkHover = "#cfcfcf2e";
const defaultHover = "#cfcfcf45";
const darkmodeActive = "#e96bff59";
const defaultActive = "#f5bdff";

const CustomButtonRoot = styled('button')`
	font-family: IBM Plex Sans, sans-serif;
	font-weight: bold;
	font-size: 0.875rem;
	background-color: transparent;
	padding: 12px 8px;
	border-radius: 8px;
	color: #050505;
	transition: all 200ms ease;
	cursor: pointer;
	border: none;
	width: 100%;

	&:hover {
		background-color: ${defaultHover};
	}
	.active & {
		background-color: ${darkmodeActive};
	}
`;

export default function CustomMessageButton(props: ButtonUnstyledProps) {
	return <ButtonUnstyled {...props} component={CustomButtonRoot} />;
}