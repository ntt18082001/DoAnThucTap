import { LoadingButton } from "@mui/lab";
import { createTheme, styled, TextField, InputBase } from '@mui/material';

const theme = createTheme();

export const CssTextField = styled(TextField)({
	'& label.Mui-focused': {
		color: theme.palette.secondary.main,
	},
	'& label.MuiInputLabel-root': {
		color: theme.palette.secondary.main
	},
	'& .MuiInput-underline:after': {
		borderBottomColor: theme.palette.secondary.main,
	},
	'& .MuiOutlinedInput-root': {
		color: theme.palette.secondary.main,
		'& fieldset': {
			borderColor: theme.palette.secondary.main,
		},
		'&:hover fieldset': {
			borderColor: theme.palette.secondary.main,
		},
		'&.Mui-focused fieldset': {
			borderColor: theme.palette.secondary.main,
		},
	},
});

export const CssInputBase = styled(InputBase)({
	'&.MuiInputBase-root': {
		color: theme.palette.secondary.light,
	}
});

export const CssLoadingButton = styled(LoadingButton)({
	'&.MuiLoadingButton-loading.Mui-disabled': {
		color: "#fff",
		boxShadow: "none",
		backgroundColor: theme.palette.secondary.dark,
	}
});