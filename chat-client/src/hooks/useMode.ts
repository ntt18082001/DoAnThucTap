import { useEffect, useState } from "react";
import { LayoutSetting } from '../models/layoutsettings.model';
import { defaultColor, defaultBorderColor, defaultBgColor, darkmodeColor, darkmodeBorderColor, darkmodeBgColor, darkmodeColorMsg } from '../constants/index';

const useMode = (mode: boolean)=> {
	const [layoutSetting, setLayoutSetting] = useState<LayoutSetting>();

	useEffect(() => {
		if (mode) {
			setLayoutSetting({
				color: darkmodeColor,
				borderColor: darkmodeBorderColor,
				bgColor: darkmodeBgColor,
				colorMsg: darkmodeColorMsg
			});
		} else {
			setLayoutSetting({
				color: defaultColor,
				borderColor: defaultBorderColor,
				bgColor: defaultBgColor,
				colorMsg: defaultColor
			});
		}
	}, [mode]);

	return layoutSetting;
};

export default useMode;